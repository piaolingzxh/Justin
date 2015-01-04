//psqlJDBC 
//Publisher :	PostgreSQL Global Development Group
//驱动地址：http://jdbc.postgresql.org/download.html => http://jdbc.postgresql.org/download/postgresql-9.3-1102.jdbc41.jar
//本地下载：http://files.cnblogs.com/piaolingzxh/postgresql-9.3-1102.jdbc41.jar.zip
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Statement;

public class postgres_jdbc {
	public static void main(String[] args) {
		try {
			Class.forName("org.postgresql.Driver").newInstance();
			String url = "jdbc:postgresql://localhost:5432/postgres";
			Connection con = DriverManager.getConnection(url, "postgres", "pg");
			Statement st = con.createStatement();
			String sql = " select * from users ";
			ResultSet rs = st.executeQuery(sql);
			ResultSetMetaData rsmd = rs.getMetaData();
			int columnCount = rsmd.getColumnCount();
			while (rs.next()) {
				for (int i = 1; i <= columnCount; i++) {
					System.out.print(rs.getString(i) + "\t");
				}
				System.out.println();
			}
			rs.close();
			st.close();
			con.close();

		} catch (Exception ee) {
			System.out.print(ee.getMessage());
		}
	}
}
