<?php

/* 
 * Get Floor PHP File
 * Version 1.0.0.0
 */

require_once ("../../controller/core_inc.php");
require_once ("../../controller/database_inc.php");
require_once ("../../controller/tokens_inc.php");

require_once ("../../model/mdl_rooms.php");

$core = new Core();
$database = new Database();
$tokens = new Tokens();
$room;

$data = (object)array();
 
header("Content-Type: text/json; charset=UTF-8"); 
header("Connection: close"); 

if(isset($_POST['auth_token'])){    
    $authToken = $core->cleanInput($_POST['auth_token']);
    $tokenData = $tokens->validateAuthToken($authToken);    
    $tokenData = json_decode($tokenData);        
    
    if($tokenData->response_code == "200"){
        $username = $core->cleanInput($tokenData->token->username);  
        
        if(isset($_POST['qr_code'])){  
            $qr_code = urldecode($_POST['qr_code']);
            $qr_code = $core->cleanInput($qr_code);            
            
            
            if($database->numberOfRecords("qr_id", "tbl_altLog", "WHERE qr_code = '$qr_code'") > 0){
                if($database->updateRow("tbl_altLog", "SET auth_token = '$authToken'", "WHERE  qr_code = '$qr_code'")){                    
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "QR Data Recognized"
                    ); 
                    http_response_code(200);
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
                    "message" => "No QR Data"
                ); 
               http_response_code(406);
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