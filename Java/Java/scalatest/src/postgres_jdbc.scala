//psqlJDBC 
//Publisher : PostgreSQL Global Development Group
//驱动地址：http://jdbc.postgresql.org/download.html => http://jdbc.postgresql.org/download/postgresql-9.3-1102.jdbc41.jar
//本地下载：http://files.cnblogs.com/piaolingzxh/postgresql-9.3-1102.jdbc41.jar.zip
import java.sql.{ Connection, DriverManager, ResultSet };

object postgres_jdbc {
  val conn_str = "jdbc:postgresql://localhost:5432/postgres"
  classOf[org.postgresql.Driver]
  def main(args: Array[String]) {
    //classOf[org.postgresql.Driver]

    val conn = DriverManager.getConnection(conn_str, "postgres", "pg")
    try {
      // Configure to be Read Only
      val statement = conn.createStatement(ResultSet.TYPE_FORWARD_ONLY, ResultSet.CONCUR_READ_ONLY)

      // Execute Query
      val rs = statement.executeQuery("SELECT * FROM users")
      var columnCount = rs.getMetaData().getColumnCount();
      // Iterate Over ResultSet
      while (rs.next) {
        for (i <- 1 to columnCount) {
          System.out.print(rs.getString(i) + "\t");
        }
        System.out.println();
      }
    } finally {
      conn.close
    }
  }
}