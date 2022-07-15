<?php

/* 
 * Add User Device PHP File
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
        if(isset($_POST['device_id'],$_POST['floor_id'],$_POST['dev_name'],$_POST['dev_desc'],$_POST['loc_xy'],$_POST['power_state'],$_POST['is_door'],$_POST['rotate_state'])){ 
            $device_id = $core->cleanInput($_POST['device_id']);
            $floorId = $core->cleanInput($_POST['floor_id']);
            $deviceName = $core->cleanInput($_POST['dev_name']);
            $dev_desc = $core->cleanInput($_POST['dev_desc']);
            $loc = $core->cleanInput($_POST['loc_xy']);
            $powerState = $core->cleanInput($_POST['power_state']);
            $roomId = NULL;
            $isdoor = $core->cleanInput($_POST['is_door']);
            $rotateState = $core->cleanInput($_POST['rotate_state']);
            
            $insertQuery = "NULL, $device_id, $floorId, NULL, '$deviceName', '$dev_desc', '$loc', $powerState, $isdoor, $rotateState";
            $searchQuery = "WHERE f.houseId = h.houseId AND h.username = '$username' AND f.floorId = $floorId";
            $tables = "tbl_floors f, tbl_houses h, tbl_rooms r";
            
            if(isset($_POST['room_id'])){
                $roomId = $core->cleanInput($_POST['room_id']);
                if($roomId == "null"){
                    $roomId = NULL;
                    $searchQuery = "WHERE f.houseId = h.houseId AND h.username = '$username' AND f.floorId = $floorId";
                    $tables = "tbl_floors f, tbl_houses h";
                }else{
                    $tables = "tbl_floors f, tbl_houses h, tbl_rooms r";
                    $insertQuery = "NULL, $device_id, $floorId, $roomId, '$deviceName', '$dev_desc', '$loc', $powerState, $isdoor, $rotateState";
                    $searchQuery = "WHERE f.floorId = r.floorId AND f.houseId = h.houseId AND r.roomId = $roomId AND h.username = '$username' AND f.floorId = $floorId";
                }
            }      
            
            if($database->numberOfRecords("deviceId", "tbl_devices", "WHERE deviceId = $device_id AND username = '$username' and assigned = 'NO'")){  
                if($database->numberOfRecords("f.floorId", $tables, $searchQuery)){      
                    if($database->insert("tbl_userDevice", $insertQuery)){
                        $userDevice = $database->lastInsertedId();
                        if($database->updateRow("tbl_devices", "SET assigned = 'YES'", "WHERE deviceId = $device_id AND username = '$username'")){        
                            $data = array(
                                "status" => true,
                                "response_code" => "200",
                                "message" => "A New Device Added to the Floor",
                                "data" =>(object)array(
                                    "user_device_id" => $userDevice,
                                    "dev_id" => $device_id,
                                    "dev_name" => "$deviceName",
                                    "dev_desc" => "$dev_desc",
                                    "location" => "$loc",
                                    "floor_id" => $floorId,
                                    "room_id" => $roomId,
                                    "power_state" => $powerState,
                                    "rotate_state" => $rotateState
                                )
                            );
                            http_response_code(200);
                        }else{
                            $data = array(
                                "status" => false,
                                "response_code" => "500",
                                "message" => "Internal Server Error Occurred.1"
                            );
                            http_response_code(500);
                        }                    
                    }else{
                        $data = array(
                            "status" => false,
                            "response_code" => "500",
                            "message" => "Internal Server Error Occurred.2"
                        );
                        http_response_code(500);
                    }                
                }else{
                    $data = array(
                        "status" => false,
                        "response_code" => "400",
                        "message" => "Device Already Assigned"
                    );
                    http_response_code(400);
                } 
            }else{                
                $data = array(
                    "status" => false,
                    "response_code" => "400",
                    "message" => "Device Already Assigned"
                );
                http_response_code(400);
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