<?php

/* 
 * Add Floor PHP File
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
        
        if(isset($_POST['device_id'], $_POST['usage'])){
            $deviceId = $core->cleanInput($_POST['device_id']);
            $usage = $core->cleanInput($_POST['usage']);
            
            if($database->numberOfRecords("userDevices", "tbl_userdevice", "WHERE userDevices = $deviceId") > 0){            
                $dev_type = $database->result("dv.devType", "tbl_userdevice ud, tbl_devices d, tbl_deviceversions dv", "where ud.deviceId = d.deviceId and d.versionId = dv.versionId and ud.userDevices = $deviceId");            
                if($dev_type == "Water Tap"){
                    $dev_type = "W";
                }else{
                    $dev_type = "E";                    
                }
                
                $priceId = $database->result("priceId", "tbl_powerPrices", "where powerType = '$dev_type' ORDER BY dateIntro LIMIT 1");
                
                if($database->insert("tbl_usageData", "NULL, $priceId, $deviceId, CURRENT_TIMESTAMP, $usage")){
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "Usage Data Added"
                    );
                    http_response_code(200);
                }
            }else{
                $data = array(
                    "status" => false,
                    "response_code" => "404",
                    "message" => "No Such Device Found"
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