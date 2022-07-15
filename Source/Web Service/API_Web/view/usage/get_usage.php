<?php

/* 
 * Get Floor PHP File
 * Version 1.0.0.0
 */

require_once ("../../controller/core_inc.php");
require_once ("../../controller/database_inc.php");
require_once ("../../controller/tokens_inc.php");

require_once ("../../model/mdl_floors.php");

$core = new Core();
$database = new Database();
$tokens = new Tokens();
$floor;

$electricity = new ElectricityUsage();
$water = new WaterUsage();

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
        
        if(isset($_GET['start_date'], $_GET['end_date'], $_GET['power_type'])){
            $start_date = $core->cleanInput($_GET['start_date']);
            $end_date = $core->cleanInput($_GET['end_date']);
            $power_type = $core->cleanInput($_GET['power_type']);
            
            $date1 = new DateTime($start_date);
            $date2 = new DateTime($end_date);

            $days = $date2->diff($date1)->format("%a");
            
            if($database->numberOfRecords("*", "tbl_usagedata ud, tbl_powerPrices p", "WHERE (p.priceId = ud.priceId) AND p.powerType = '$power_type' AND (ud.dateRecored >= '$start_date' AND ud.dateRecored <= '$end_date')") > 0){
                $result = $database->select("ud.usageId, ud.userDevices, ud.dateRecored, ud.usageData, p.powerType, p.powerPrice", "tbl_usagedata ud, tbl_powerPrices p", "WHERE (p.priceId = ud.priceId) AND p.powerType = '$power_type' AND (ud.dateRecored >= '$start_date' AND ud.dateRecored <= '$end_date') GROUP BY ud.userDevices,ud.dateRecored");
                $data=array();
                while($row = $result->fetch_assoc()) {
                    $use_id = $row['usageId'];
                    $device_id = $row['userDevices'];
                    $date_recorded = explode(" ", $row['dateRecored'])[0];
                    $powerType = $row['powerType'];      
                    $usage_data = $row['usageData'];
                    
                    $usage =  $electricity->calculateBill($usage_data, $days);
                    if($powerType == "W")
                        $usage =  $water->calculateBill($usage_data, $days);
                    
                    $powerPrice = $row['powerPrice'];
                    
                    $progress = $usage/100;
					$progress = ceil($progress);
                    $direction = "+";
                    if($usage >= 2000)
                        $direction = "-";

                    $data[] = array(
                        "usage_id" => $use_id,
                        "device_id" => $device_id,
                        "date_recorded" => "$date_recorded",
                        "price" => $usage,
                        "power_type" => "$powerType",
                        "units" => $usage_data,
                        "progress_data" => array(
                            "progress" => $progress,
                            "direction" => $direction                           
                        )
                    );
                }
                
                $data = array(
                    "status" => true,
                    "response_code" => "200",
                    "message" => "Usage Data",
                    "data" => $data
                ); 
                http_response_code(200);
            }else{
                $data = array(
                    "status" => false,
                    "response_code" => "404",
                    "message" => "No Usage Data",
                    "data" => $data
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