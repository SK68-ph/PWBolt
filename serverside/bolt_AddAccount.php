<?php
    session_start();
    // Check if post request is valid and provided
    if( !( isset($_POST['website']) &&  isset($_POST['username']) &&  isset($_POST['password']) &&  isset($_SESSION['guid']) &&  isset($_SESSION['pwbolt']) &&  isset($_SESSION['loggedin'])))
    {
     die("empty params Failed");
    }
    // Init Database connection
    require_once 'config/config.php';

    // Trim our post request to remove unwanted whitespace
    $website = trim($_POST["website"]);
    $username = trim($_POST["username"]);
    $guid = $_SESSION['guid'];
    $userTable = "tbl_userVault".$guid;
    // Encypt the plain text password using AES_ENCRYPTION
    // key for decrypting is as follows
    // KEY = userPassword + salt
    $Cryptor = new \Vendor\Library\Cryptor($_SESSION['pwbolt'].$salt);
    $crypted = $Cryptor->encrypt(trim($_POST["password"]));
    // Insert website,username and the encrypted password to our database.
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
