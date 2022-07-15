<?php

/* 
 * Update Device PHP File
 * Version 1.0.0.0
 */

require_once ("../../controller/core_inc.php");
require_once ("../../controller/database_inc.php");
require_once ("../../controller/tokens_inc.php");

$core = new Core();
$database = new Database();
$tokens = new Tokens();

$data = (object)array();
 
header("Content-Type: text/json; charset=UTF-8"); 
header("Connection: close"); 

if(isset($_POST['auth_token'])){    
    $authToken = $core->cleanInput($_POST['auth_token']);
    $tokenData = $tokens->validateAuthToken($authToken);    
    $tokenData = json_decode($tokenData);        
    
    if($tokenData->response_code == "200"){
        $username = $core->cleanInput($tokenData->token->username);  
        $username = $core->getParentUser($username);
        if(isset($_POST['device_id'], $_POST['assigned'])){ 
            $assigned = strtoupper($core->cleanInput($_POST['assigned']));
            $deviceId = $core->cleanInput($_POST['device_id']);
            
            if($database->numberOfRecords("deviceId", "tbl_devices", "WHERE deviceId = $deviceId AND username = '$username'")){
                if($assigned == "YES" || $assigned == "NO"){
                    $query = "SET assigned = '$assigned'";
                    if($database->updateRow("tbl_devices", $query, "WHERE deviceId = $deviceId AND username = '$username'")){   
                        $auth_code = $core->generateUniqueCode(19);
                        
                        if($database->numberOfRecords("auth_id", "tbl_deviceAuths", "WHERE deviceId = $deviceId") > 0){
                            $data = array(
                                "status" => true,
                                "response_code" => "200",
                                "message" => "Device Updated Successfully",
                                "data" => array(
                                    "device_assigned" => "$assigned"
                                )
                            );
                            http_response_code(200);                            
                        }else{
                            if($database->insert("tbl_deviceAuths", "NULL, $deviceId, '$auth_code', CURRENT_TIMESTAMP")){
                                $data = array(
                                    "status" => true,
                                    "response_code" => "200",
                                    "message" => "Device Updated Successfully",
                                    "data" => array(
                                        "device_assigned" => "$assigned"
                                    )
                                );
                                http_response_code(200);
                            }else{
                                $database->updateRow("tbl_devices", "SET assigned = 'NO'", "WHERE deviceId = $deviceId AND username = '$username'");
                                $data = array(
                                    "status" => false,
                                    "response_code" => "500",
                                    "message" => "Internal Server Error Occurred."
                                );
                                http_response_code(500);
                            } 
                        }                      
                    }else{
                        $data = array(
                            "status" => false,
                            "response_code" => "500",
                            "message" => "Internal Server Error Occurred."
                        );
                        http_response_code(500);
                    }
                }else{
                    $data = array(
                        "status" => false,
                        "response_code" => "400",
                        "message" => "Bad Request"
                    );
                    http_response_code(400);
                }                
            }else{
                $data = array(
                    "status" => false,
                    "response_code" => "404",
                    "message" => "No Such Device Registered to $username"
                );
                http_response_code(404);
            }
        }else{
           $data = array(
                "status" => false,
                "response_code" => "406",
                "message" => "Insuffient Data"
            ); 
           http_response_code(406);
        }        
    }else{
        $data = array(
            "status" => false,
            "response_code" => "$tokenData->response_code",
            "message" => "$tokenData->message"
        ); 
        http_response_code($tokenData->response_code);
    }   
}else{
    $data = array(
        "status" => false,
        "response_code" => "400",
        "message" => "Bad Request"
    );
    http_response_code(400);
}
echo json_encode($data);