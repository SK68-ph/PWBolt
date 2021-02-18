<?php
    session_start();
    // Check if post request is valid and provided
    if( !(isset($_POST['username']) &&  isset($_POST['pwbolt'])))
    {
     die("empty params Failed");
    }
    // Init Database connection
    require_once 'config/config.php';

    // Trim our post request to remove unwanted whitespace
    $username = trim($_POST["username"]);
    $pwbolt = trim($_POST["pwbolt"]);

    // Prepare Checking of username in database
    if ($stmt = $link->prepare("SELECT guid,pwbolt from tbl_users WHERE username=?")) {
        $stmt->bind_param('s', $username);
        if ($stmt->execute()) {
          $stmt->store_result();
          // Check if username exists. then verify
          if ($stmt->num_rows == 1) {
              $guid = ''; // see register.php for more info about guid
              $verifyPass = '';
              $stmt->bind_result($guid,$verifyPass);
              $stmt->fetch();
              if(password_verify($pwbolt, $verifyPass)) { // see php documentation about password_verify and password_hash for more info
                echo "Status:OK, Login Success."; // output to client that we are loggedin
                  $_SESSION['loggedin'] = true;
                  $_SESSION['guid'] = $guid;
                  $_SESSION['pwbolt'] = $pwbolt; // this will be used as a key for Encrypting account's password later on.
              }else
              echo "Status:ERROR, Incorrect Password";
          }else
              echo "Status:ERROR, Account Not Found";
        }else
          echo "Status:ERROR, Failed executing query";

        $stmt->close();

    }else
      echo "Status:ERROR, Failed intitiating query";

    mysqli_close($link);
    //https://elucidative-designa.000webhostapp.com/PWBOLT_register.php
?>
