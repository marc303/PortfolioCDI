<?php
  require_once 'header.php';
  require_once 'login.php';
  
  try {
    $pdo = new PDO($attr, $user, $pass, $opts);
  } 
  catch (PDOException $e) {
    throw new PDOException($e->getMessage(), (int)$e-getCode());
  }

  $query = "SELECT r.id, e.codeCours, c.description, e.Evaluation, r.note FROM cours c INNER JOIN evaluations e ON c.id = e.codeCours INNER JOIN resultats r ON e.id = r.id";
    $result = $pdo->query($query);
  
    echo <<<_END
      <h3>Choissisez une évaluation à modifier ou supprimer</h3>
      <table><tr><th>id</th><th>description</th><th>evaluation</th><th>Note</th><th>Action</th></tr>
    _END;

    while ($row = $result->fetch(PDO::FETCH_BOTH)) {
  
      $id = htmlspecialchars($row['id']);
      $code = htmlspecialchars($row['codeCours']);
      $desc = htmlspecialchars($row['description']);
      $eval = htmlspecialchars($row['Evaluation']);
      $note = htmlspecialchars($row['note']);
  
      echo <<<_END
        <tr>
        <td>$code</td>
        <td>$desc</td>
        <td>$eval</td>
        <td>$note %</td>
        <td><form action="form.php" method="post">
        <input type="hidden" name="evalID" value="$id">
        <input type="submit" name="upEval" value="Modifier">
        <input type="submit" name="delEval" value="Supprimer">
      </form></td>
        </tr>
      _END;
    }
    echo <<<_END
        </table>
        <button
          onclick="window.location.href='index.php';"
        >Annuler</button>
    _END;
?>


<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
  <link rel="stylesheet" href="css/styles.css">
</head>
<body>

</body>
</html>