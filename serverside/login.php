<?php
    if( !(isset($_POST['username']) &&  isset($_POST['pwbolt'])))
    {
     die("empty params Failed");
    }

    $username = trim($_POST["username"]);
    $pwbolt = trim($_POST["pwbolt"]);
    require_once 'config/config.php';

    if ($stmt = $link->prepare("SELECT guid,pwbolt from tbl_users WHERE username=?")) {
        $stmt->bind_param('s', $username);
        if ($stmt->execute()) {
          $stmt->store_result();
          // Check if username exists. Verify user exists then verify
          if ($stmt->num_rows == 1) {
              $guid = '';
              $verifyPass = '';
              $stmt->bind_result($guid,$verifyPass);
              $stmt->fetch();
              if(password_verify($pwbolt, $verifyPass)) {
                echo "Status:OK, Login Success.";
                  session_start();
                  $_SESSION['loggedin'] = true;
                  $_SESSION['guid'] = $guid;
                  $_SESSION['pwbolt'] = $pwbolt;
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
