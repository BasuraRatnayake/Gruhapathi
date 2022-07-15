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

if(isset($_POST['qr_code'])){
    echo "asdas";
    $qrCode = $core->cleanInput($_POST['qr_code']);    
    $current_ip = getenv('HTTP_CLIENT_IP')? : getenv('HTTP_X_FORWARDED_FOR')?: getenv('HTTP_X_FORWARDED')?: getenv('HTTP_FORWARDED_FOR')?: getenv('HTTP_FORWARDED')? : getenv('REMOTE_ADDR');
    
    if($database->numberOfRecords("qr_id", "tbl_altLog", "WHERE ip_address = '$current_ip'") > 0){
        $database->delete("tbl_altLog", "WHERE ip_address = '$current_ip'");
    }
    
    if($database->insert("tbl_altLog", "NULL, '$qrCode', CURRENT_TIMESTAMP, '0', '$current_ip'")){
        $qr_id = $database->lastInsertedId();
        $data = array(
            "status" => true,
            "response_code" => "200",
            "message" => "QR Code Registered",
            "data" => (object)array(
                "qr_code_id" => $qr_id,
                "qr_code" => "$qrCode",
                "ip_address" => "$current_ip"
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
        "response_code" => "406",
        "message" => "Insuffient Data"
    );
    http_response_code(406);
}   

echo json_encode($data);