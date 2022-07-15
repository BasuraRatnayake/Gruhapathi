<?php
header("Content-Type: text/json;"); 
header("Access-Control-Allow-Origin: *"); 
header("Access-Control-Allow-Credentials: true"); 

if(isset($_GET['username'], $_GET['password'])){
	echo "{"."status"." : true, "."useOk:true"."}"
        http_response_code(200);
}else{
	http_response_code(404);
}
	http_response_code(404);
?>