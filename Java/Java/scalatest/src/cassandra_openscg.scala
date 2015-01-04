//驱动地址：https://bitbucket.org/openscg/cassandra2-jdbc/branch/2.1.1
//或者http://download.csdn.net/detail/piaolingzxh/8320131
import java.sql.DriverManager
import org.bigsql.cassandra2.jdbc.CassandraDriver

object cassandra_openscg {

  import java.sql.{ Connection, DriverManager, ResultSet }

  def main(args: Array[String]): Unit = {
    classOf[org.bigsql.cassandra2.jdbc.CassandraDriver]
    val db = DriverManager.getConnection("jdbc:cassandra://127.0.0.1:9160/demo")
    val st = db.createStatement
    val res = st.executeQuery("select * from demo.users")
    while (res.next) {
      for (i <- 1 to res.getMetaData.getColumnCount) {
        val r = res.getString(i).toString
        print("\t" + r)
      }
      println
    }
    db.close
  }
}