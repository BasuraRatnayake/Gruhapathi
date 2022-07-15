<?php

/* 
 * Update Floor PHP File
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
        if(isset($_POST['device_id'], $_POST['dev_name'], $_POST['dev_desc'], $_POST['loc_xy'], $_POST['rotate_state'], $_POST['power_state'])){ 
            $deviceId = $core->cleanInput($_POST['device_id']);
            $devName = $core->cleanInput($_POST['dev_name']);
            $devDes = $core->cleanInput($_POST['dev_desc']);
            $loc = $core->cleanInput($_POST['loc_xy']);
            $rotate = $core->cleanInput($_POST['rotate_state']);
            $power = $core->cleanInput($_POST['power_state']);
            
            if($database->numberOfRecords("userDevices", "tbl_userDevice", "WHERE userDevices = $deviceId")){               
                $query = "SET devName = '$devName', devDes = '$devDes', loc_xy = '$loc', powerState = $power, rotateState = $rotate";                
                $current_powerState = $database->result("powerState", "tbl_userDevice", "WHERE userDevices = $deviceId");
                
                if($database->updateRow("tbl_userDevice", $query, "WHERE userDevices = $deviceId")){             
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "Device Updated Successfully",
                        "data" => array(
                            "device_id" => $deviceId,
                            "dev_name" => "$devName",
                            "dev_desc" => "$devDes",
                            "loc_xy" => "$loc",
                            "rotate_state" => $rotate,
                            "power_state" => $power
                        )
                    );
                    http_response_code(200);
                    
                    $ori_deviceId = $database->result("deviceId", "tbl_userDevice", "WHERE userDevices = $deviceId");
                    if($current_powerState != $power){
                        if($database->insert("tbl_commands", "NULL, '$username', $ori_deviceId, '$power', CURRENT_TIMESTAMP")){
                            $cmdId = $database->lastInsertedId();
                            if(!$database->insert("tbl_cmdRequests", "NULL, $cmdId, '0'")){
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