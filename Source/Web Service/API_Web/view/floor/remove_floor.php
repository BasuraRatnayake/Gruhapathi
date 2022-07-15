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
        if(isset($_POST['floor_id'])){ 
            $roomId;
            $floorId = $core->cleanInput($_POST['floor_id']);
            
            $query = "WHERE h.username = '$username'";
            if(strtolower($floorId) != "all"){
                $query = $query." and f.floorId = $floorId";
            }
            
            if($database->numberOfRecords("h.houseId", "tbl_houses h, tbl_floors f", $query)){
                $roomId = $database->result("houseId", "tbl_houses", "WHERE username = '$username'");     
                if(strtolower($floorId) == "all"){
                    $query = "WHERE houseId = $roomId";
                }else{
                    $query = "WHERE houseId = $roomId and floorId = $floorId";
                }
                
                if($database->delete("tbl_floors", $query)){
                    if(strtolower($floorId) == "all"){
                        $query = "All Floors Belongs to House: $roomId were Successfully Deleted";
                    }else{                        
                        $query = "Floor: $floorId of House: $roomId was Successfully Deleted";
                    }
                    $data = array(
                        "status" => true,
                        "response_code" => "200",
                        "message" => "Floor Deleted Successfully",
                        "data" => $query
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