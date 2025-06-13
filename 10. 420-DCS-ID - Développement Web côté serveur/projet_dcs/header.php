<?php
  require_once 'login.php';

  try {
    $pdo = new PDO($attr, $user, $pass, $opts);
  } 
  catch (PDOException $e) {
    throw new PDOException($e->getMessage(), (int)$e-getCode());
  }

  $query = "SELECT ROUND(AVG(note),2) AS moyenne FROM resultats";
  $result = $pdo->query($query); 

  $row = $result->fetch(PDO::FETCH_BOTH);
  $avg = htmlspecialchars($row['moyenne']);

  echo <<<_END
  <div id="header">
    <div class="header">
      <span class="bold">Numéro de l'étudiant:</span> 645357336
    </div>
    <div class="header">
      <span class="bold">Nom de l'étudiant:</span> Marc-André Marchand
    </div>
    <div class="header">
      <span class="bold">Nom du programme:</span> Programmeur-analyste – LEA.9C
    </div>
    <div class="header">
      <span class="bold">Moyenne de toutes les notes:</span> $avg %
    </div>
  </div>
  <hr />
  _END;

  function mysql_fix_string($string)
  {
    if (get_magic_quotes_gpc()) {
      $string = stripslashes($string);
    }
    return $string;
  }
?>  