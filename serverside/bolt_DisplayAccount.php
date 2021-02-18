<?php
    session_start();

    // Check if our global vars exist
    if( !(isset($_SESSION['guid']) &&  isset($_SESSION['pwbolt'])))
    {
     die("empty params Failed");
    }
    // Init Database connection
    require_once 'config/config.php';

    // Init vars that will be used later on.
    $guid = $_SESSION['guid'];
    $loggedin = $_SESSION['loggedin'];
    $pwbolt = $_SESSION['pwbolt'].$salt; // we use our user's password and a salt for decrypting
    $userTable = "tbl_userVault".$guid;

    // Select all of the stored information in the users own table.
    if ($stmt = $link->prepare("SELECT * from {$userTable}")) {
        if ($stmt->execute()) {
          $stmt->store_result();
          if ($stmt->num_rows >= 1) { // if theres no data to display, simply output a warning message.
            $out_id    = NULL;
            $out_website = NULL;
            $out_username = NULL;
            $out_password = NULL;
            if (!$stmt->bind_result($out_id, $out_website,$out_username, $out_password)) {
              echo "Binding output parameters failed: (" . $stmt->errno . ") " . $stmt->error;
            }
              $Cryptor = new \Vendor\Library\Cryptor($pwbolt);
              while ($stmt->fetch()) { // itterate and decrypt password from the database and then output as plain text.
                $decrypted = $Cryptor->decrypt($out_password);
                echo "ID : {$out_id}  WEBSITE : {$out_website}  USERNAME : {$out_username}  PASSWORD: {$decrypted} \r\n";
              }
            }else
              echo "Status:WARNING, Empty Data";
          }else
              echo "Status:ERROR, Failed Executing Query";
        }else
          echo "Status:ERROR, Account Not Found";
    $stmt->close();
    mysqli_close($link);
    //https://elucidative-designa.000webhostapp.com/PWBOLT_register.php
?>
