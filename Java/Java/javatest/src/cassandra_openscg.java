//驱动地址：https://bitbucket.org/openscg/cassandra2-jdbc/branch/2.1.1
//或者http://download.csdn.net/detail/piaolingzxh/8320131
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
 
public class cassandra_openscg {
    public static void main(String[] a) {
        try {
            Class.forName("org.bigsql.cassandra2.jdbc.CassandraDriver");
            Connection con = DriverManager
                    .getConnection("jdbc:cassandra://127.0.0.1:9160/demo");
 
            String query = "select * from demo.users";
 
            Statement statement = con.createStatement();
            ResultSet rs = statement.executeQuery(query);
 
            while (rs.next()) {
                System.out.print(rs.getString(1) + ":" + rs.getString(2) + "\t"
                        + rs.getString(3) + "\t" + rs.getString(4) + "\t"
                        + rs.getString(5) + "\t" + rs.getString(6) + "\t"
                        + "\n");
            }
 
            rs.close();
            statement.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
 
    }
}