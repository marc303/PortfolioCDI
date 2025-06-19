<?php
  require_once 'header.php';
  require_once 'login.php';

  try {
    $pdo = new PDO($attr, $user, $pass, $opts);
  } 
  catch (PDOException $e) {
    throw new PDOException($e->getMessage(), (int)$e-getCode());
  }

  if (isset($_POST['addNote'])) {
    $evalID = mysql_fix_string($_POST['eval']);
    $note = mysql_fix_string($_POST['note']);

    $inst = $pdo->prepare('INSERT INTO resultats VALUES(?,?)');
    $inst->bindParam(1, $evalID, PDO::PARAM_INT);
    $inst->bindParam(2, $note, PDO::PARAM_INT);
    $inst->execute();
  }
  elseif (isset($_POST['upNote'])) {
    $evalStart = mysql_fix_string($_POST['evalStart']);
    $evalEnd = mysql_fix_string($_POST['evalEnd']);
    $note = mysql_fix_string($_POST['note']);

    $inst = $pdo->prepare('UPDATE resultats SET id=?, note=? WHERE id=?');
    $inst->bindParam(1, $evalEnd, PDO::PARAM_INT);
    $inst->bindParam(2, $note, PDO::PARAM_INT);
    $inst->bindParam(3, $evalStart, PDO::PARAM_INT);
    $inst->execute();
  }
  elseif (isset($_POST['delNote'])) {
    $evalID = mysql_fix_string($_POST['evalID']);

    $inst = $pdo->prepare('DELETE FROM resultats WHERE id=?');
    $inst->bindParam(1, $evalID, PDO::PARAM_INT);
    $inst->execute();
  }

  $query = "SELECT e.codeCours, c.description, ROUND(AVG(r.note),2) AS moyenne  FROM cours c INNER JOIN evaluations e ON c.id = e.codeCours INNER JOIN resultats r ON e.id = r.id GROUP BY(e.codeCours)";
  $result = $pdo->query($query);

  echo <<<_END
    <h3>Moyenne des notes par cours</h3>
    <table><tr><th>id</th><th>description</th><th>Note</th></tr>
  _END;

  while ($row = $result->fetch(PDO::FETCH_BOTH)) {

    $id = htmlspecialchars($row['codeCours']);
    $desc = htmlspecialchars($row['description']);
    $avg = htmlspecialchars($row['moyenne']);

    echo <<<_END
      <tr>
      <td>$id</td>
      <td>$desc</td>
      <td>$avg %</td>
      </tr>
    _END;
  }
  echo <<<_END
      </table>
      
  _END;

  if (isset($_POST['showAll'])) {
    
    $query = "SELECT e.codeCours, c.description, e.Evaluation, r.note FROM cours c INNER JOIN evaluations e ON c.id = e.codeCours INNER JOIN resultats r ON e.id = r.id";
    $result = $pdo->query($query);
  
    echo <<<_END
      <h3>Note obtenue pour chacune des évaluations complétées</h3>
      <table><tr><th>id</th><th>description</th><th>evaluation</th><th>Note</th></tr>
    _END;
  
    while ($row = $result->fetch(PDO::FETCH_BOTH)) {
  
      $id = htmlspecialchars($row['codeCours']);
      $desc = htmlspecialchars($row['description']);
      $eval = htmlspecialchars($row['Evaluation']);
      $note = htmlspecialchars($row['note']);
  
      echo <<<_END
        <tr>
        <td>$id</td>
        <td>$desc</td>
        <td>$eval</td>
        <td>$note %</td>
        </tr>
      _END;
    }
    echo <<<_END
        </table>
    _END;
  }
  echo <<<_END
      <br />
    <form action="form.php" method="post">
    <input
      type="submit"
      name="add"
      value="Ajouter une évaluation"
    />
    </form>
    <button
      onclick="window.location.href='update.php';"
    >Modifier une évaluation</button>
    <form action="index.php" method="post">
    <input
      type="submit"
      name="showAll"
      value="Afficher les évaluations complétées"
    />
    </form>
  _END;
?>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
    <link rel="stylesheet" href="css/styles.css">
  </head>
  <body>
  </body>
</html>
