<?php

/* 
 * Get Device PHP File
 * Version 1.0.0.0
 */

require_once ("../../controller/core_inc.php");
require_once ("../../controller/database_inc.php");
require_once ("../../controller/tokens_inc.php");

require_once ("../../model/mdl_device.php");

$core = new Core();
$database = new Database();
$tokens = new Tokens();
$device;

$data = (object)array();
 
header("Content-Type: text/json; charset=UTF-8"); 
header("Connection: close"); 

if(isset($_GET['auth_token'])){    
    $authToken = $core->cleanInput($_GET['auth_token']);
    $tokenData = $tokens->validateAuthToken($authToken);    
    $tokenData = json_decode($tokenData);        
    
    if($tokenData->response_code == "200"){
        $username = $core->cleanInput($tokenData->token->username);  
        $username = $core->getParentUser($username);
        if(isset($_GET['assigned'], $_GET['device_id'])){ 
            $assigned = strtoupper($core->cleanInput($_GET['assigned']));
            $deviceId = $core->cleanInput($_GET['device_id']);
            
            $device = new Device($username, $deviceId, $assigned);
            $data = $device->exportAsJSON();
            
            if($data == null){
                $data = array(
                    "status" => false,
                    "response_code" => "404",
                    "message" => "No Device Data",
                    "data" => $data
                ); 
                http_response_code(404);
            }else{
                $data = array(
                    "status" => true,
                    "response_code" => "200",
                    "message" => "Device Data",
                    "data" => $data
                ); 
                http_response_code(200);
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