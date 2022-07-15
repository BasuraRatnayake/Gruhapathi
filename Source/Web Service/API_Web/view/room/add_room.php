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
        if(isset($_POST['floor_id'],$_POST['room_name'],$_POST['room_desc'],$_POST['loc_xy'],$_POST['size_hw'],$_POST['rotate_state'],$_POST['power_state'])){ 
            $floorId = $core->cleanInput($_POST['floor_id']);
            $roomName = $core->cleanInput($_POST['room_name']);
            $roomDesc = $core->cleanInput($_POST['room_desc']);
            $loc_xy = $core->cleanInput($_POST['loc_xy']);
            $size_hw = $core->cleanInput($_POST['size_hw']);
            $rotateState = $core->cleanInput($_POST['rotate_state']);
            $powerState = $core->cleanInput($_POST['power_state']);
            
            if($database->numberOfRecords("floorId", "tbl_floors", "WHERE floorId = $floorId")){
                if($database->insert("tbl_rooms", "NULL, $floorId, '$roomName', '$roomDesc', '$loc_xy', '$size_hw', $rotateState, $powerState")){
                    $roomId = $database->lastInsertedId();
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "A New Room Added to the Floor",
                        "data" =>(object)array(
                            "floor_id" => "$floorId",
                            "room_id" => "$roomId",
                            "room_name" => "$roomName",
                            "room_desc" => "$roomDesc",
                            "room_loc" => "$loc_xy",
                            "room_size" => "$size_hw",
                            "rotate_state" => "$rotateState",
                            "power_state" => "$powerState"
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
                    "message" => "No Such Floor Registered to House"
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