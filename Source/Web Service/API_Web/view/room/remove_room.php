<?php

/* 
 * Remove Floor PHP File
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
        if(isset($_POST['room_id'], $_POST['floor_id'])){ 
            $roomId = $core->cleanInput($_POST['room_id']);
            $floorId = $core->cleanInput($_POST['floor_id']);
            
            $query = "WHERE h.houseId = f.houseId and f.floorId = r.floorId and h.username = '$username'";
            if(strtolower($roomId) != "all"){
                $query = $query." and r.roomId = $roomId";
            }
            if($database->numberOfRecords("f.floorId", "tbl_houses h, tbl_floors f, tbl_rooms r", $query)){
                if(strtolower($roomId) == "all"){
                    $query = "WHERE floorId = $floorId";
                }else{
                    $query = "WHERE roomId = $roomId and floorId = $floorId";
                }
                
                $doorDeviceId = $database->result("deviceId", "tbl_userDevice", "WHERE floorId = $floorId and isDoor = 1");
                
                if($database->updateRow("tbl_devices", "SET assigned = 'NO'", "WHERE deviceId = $doorDeviceId")){                    
                    if($database->delete("tbl_userDevice", "WHERE floorId = $floorId AND isDoor = 1")){
                        if($database->delete("tbl_rooms", $query)){
                            if(strtolower($roomId) == "all"){
                                $query = "All Rooms Belongs to Floor: $floorId were Successfully Deleted";
                            }else{                        
                                $query = "Room: $roomId of Floor: $floorId was Successfully Deleted";
                            }
                            $data = array(
                                "status" => true,
                                "response_code" => "200",
                               "message" => "Room Deleted Successfully",
                               "data" => $query
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
                        "response_code" => "500",
                        "message" => "Internal Server Error Occurred.3"
                    );
                    http_response_code(500);
                }
            }else{
                $data = array(
                    "status" => false,
                    "response_code" => "404",
                    "message" => "No Such House/Floor Registered to $username"
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