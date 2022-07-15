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

if(isset($_POST['qr_code'])){  
    $qr_code = $core->cleanInput($_POST['qr_code']);

    if($database->numberOfRecords("qr_id", "tbl_altLog", "WHERE qr_code = '$qr_code'") > 0){
        if($database->numberOfRecords("qr_id", "tbl_altLog", "WHERE qr_code = '$qr_code' AND auth_token != '0'") > 0){
            $token_tbl = $database->result("auth_token", "tbl_altLog", "WHERE qr_code = '$qr_code'");   
            $token = $database->fetchFullRow("*", "tbl_tokens", "WHERE token = '$token_tbl'");       
            
            $timeInSeconds = strtotime($token[2]);
            $data = array(
                "status" => true,
                "response_code" => "200",
                "message" => "Approval Yet",
                "token" => (object)array(
                    "auth_token" => "$token[4]",
                    "auth_expire" => $timeInSeconds,
                    "refresh_token" => "$token[8]",
                    "username" => "$token[1]"
                )
            ); 
            
            $database->delete("tbl_altLog", "WHERE qr_code = '$qr_code'");
            http_response_code(200);
        }else{
            $data = array(
                "status" => false,
                "response_code" => "401",
                "message" => "Authentication Failed"
            ); 
            http_response_code(401);
        }
    }else{
        $data = array(
            "status" => false,
            "response_code" => "404",
            "message" => "No QR Data Registered"
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

echo json_encode($data);