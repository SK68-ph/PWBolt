<?php
    session_start();
    if( !( isset($_POST['website']) &&  isset($_POST['username']) &&  isset($_POST['password']) &&  isset($_SESSION['guid']) &&  isset($_SESSION['pwbolt']) &&  isset($_SESSION['loggedin'])))
    {
     die("empty params Failed");
    }
    require_once 'config/config.php';

    $website = trim($_POST["website"]);
    $username = trim($_POST["username"]);
    $guid = $_SESSION['guid'];
    $userTable = "tbl_userVault".$guid;
    $Cryptor = new \Vendor\Library\Cryptor($_SESSION['pwbolt'].$salt);
    $crypted = $Cryptor->encrypt(trim($_POST["password"]));

    if ($stmt = $link->prepare("INSERT INTO {$userTable}(website,username,password) VALUES('{$website}','{$username}','{$crypted}')")) {
        if ($stmt->execute()) {
          echo "Status:OK, Account Created.";
        }else
           "Status:ERROR, Failed executing query";
        $stmt->close();
    }else
      echo "Status:ERROR, Failed intitiating query";
    mysqli_close($link);
?>
