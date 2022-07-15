<?php

/* 
 * Update Device Power
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
        if(isset($_POST['power_state'])){ 
            $power = $_POST['power_state'];
            $power = urldecode($power);
            $power = json_decode($power);
            
            $data = array(
                "status" => true,
                "response_code" => "200",
                "message" => "Power States Updated"
            );
            http_response_code(200);    
            
            for($i=0;$i<sizeof($power);$i++){
                $power_s = $power[$i]->power_state;
                $power_i = $power[$i]->device_id;
                $current_powerState = $database->result("powerState", "tbl_userDevice", "WHERE userDevices = $power_i");
                if(!$database->updateRow("tbl_userDevice", "SET powerState = $power_s", "WHERE userDevices = $power_i")){
                    $data = array(
                        "status" => false,
                        "response_code" => "500",
                        "message" => "Server Error"
                    );
                    http_response_code(500);    
                }
                          
                $ori_deviceId = $database->result("deviceId", "tbl_userDevice", "WHERE userDevices = $power_i");
                if($current_powerState != $power_s){
                    if($database->insert("tbl_commands", "NULL, '$username', $ori_deviceId, '$power_s', CURRENT_TIMESTAMP")){
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