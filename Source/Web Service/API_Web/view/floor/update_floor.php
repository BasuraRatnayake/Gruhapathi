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
        if(isset($_POST['house_id'], $_POST['floor_name'], $_POST['power_state'], $_POST['floor_id'])){ 
            $roomId = $core->cleanInput($_POST['house_id']);
            $floorName = $core->cleanInput($_POST['floor_name']);
            $powerState = $core->cleanInput($_POST['power_state']);
            $floorId = $core->cleanInput($_POST['floor_id']);
            
            if($database->numberOfRecords("h.houseId", "tbl_houses h, tbl_floors f", "WHERE h.houseId = f.houseId and h.username = '$username' and f.floorId = $floorId")){
                $query = "SET floorname = '$floorName', powerState = $powerState";
                if($database->updateRow("tbl_floors", $query, "WHERE houseId = $roomId AND floorId = $floorId")){                     
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "Floor Updated Successfully",
                        "data" => array(
                            "floor_name" => "$floorName",
                            "power_state" => $powerState
                        )
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
                    "message" => "No Such House Registered to $username"
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