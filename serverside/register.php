<?php
    if( !(isset($_POST['username']) &&  isset($_POST['pwbolt'])))
    {
     die("empty params Failed");
    }
    require_once 'config/config.php';

    $username = trim($_POST["username"]);
    $pwbolt = trim($_POST["pwbolt"]);

    $hashedPw = password_hash($pwbolt, PASSWORD_BCRYPT);
    if(password_verify($pwbolt, $hashedPw)) {
      $random = random_int(1,2147483047);
      $sql = "INSERT INTO tbl_users() VALUES('{$random}', '{$username}','{$hashedPw}')";
      if ($stmt = $link->prepare($sql)) {
          if ($stmt->execute()) {
            echo "Status:OK , Register Done";
            $userTable = "tbl_userVault".$random;
            $sql2 = "CREATE TABLE IF NOT EXISTS {$userTable}(id int PRIMARY KEY AUTO_INCREMENT,website varchar(100),username varchar(100),password varchar(600))";
            if ($stmt2 = $link->prepare($sql2)) {
              $stmt2->execute();
              $stmt2->close();
            }
          }else {
            echo "Status:ERROR, Username already taken";
          }
          $stmt->close();
        }
      }
    mysqli_close($link);
    //#https://elucidative-designa.000webhostapp.com/PWBOLT_register.php
?>
