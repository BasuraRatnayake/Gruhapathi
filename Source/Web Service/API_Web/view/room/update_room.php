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
        if(isset($_POST['floor_id'], $_POST['room_id'], $_POST['room_name'], $_POST['room_desc'], $_POST['loc_xy'], $_POST['size_hw'], $_POST['rotate_state'], $_POST['power_state'])){ 
            $floorId = $core->cleanInput($_POST['floor_id']);
            $roomId = $core->cleanInput($_POST['room_id']);
            $roomName = $core->cleanInput($_POST['room_name']);
            $roomDesc = $core->cleanInput($_POST['room_desc']);
            $roomLoc = $core->cleanInput($_POST['loc_xy']);
            $roomSize = $core->cleanInput($_POST['size_hw']);
            $roomRot = $core->cleanInput($_POST['rotate_state']);
            $roomPower = $core->cleanInput($_POST['power_state']);
            
            if($database->numberOfRecords("h.houseId", "tbl_houses h, tbl_floors f, tbl_rooms r", "WHERE h.houseId = f.houseId and f.floorId = r.floorId and h.username = '$username' and r.roomId = $roomId")){
                
                $query = "SET roomName = '$roomName', roomDesc = '$roomDesc', loc_xy = '$roomLoc', size_hw = '$roomSize', rotateState = $roomRot, powerState = $roomPower";
                if($database->updateRow("tbl_rooms", $query, "WHERE floorId = $floorId AND roomId = $roomId")){    
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "Room Updated Successfully",
                        "data" => array(
                            "room_name" => "$roomName",
                            "power_state" => $roomPower,
                            "room_desc" => "$roomDesc",
                            "room_loc" => "$roomLoc",
                            "room_size" => "$roomSize",
                            "room_rotate" => "$roomRot"
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