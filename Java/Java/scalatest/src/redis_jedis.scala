//redis -> jedis
//驱动地址：https://raw.githubusercontent.com/xetorthio/jedis/mvn-repo/redis/clients/jedis/2.2.0/jedis-2.2.0.jar
//或者http://files.cnblogs.com/piaolingzxh/jedis-2.2.0.jar.zip
import redis.clients.jedis.Jedis;

object redis_jedis {
  def main(args: Array[String]): Unit = {
    var redis = new Jedis("localhost", 6379);
    // String
    redis.del("user");
    redis.set("user", "piaolingzxh");
    System.out.println(redis.get("user"));

    // List
    redis.del("edu");
    redis.lpush("edu", "xiaoxue", "chuzhong", "gaozhong");
    redis.lrange("edu", 0, -1).toArray().foreach((str) => System.out.print(str + "\t"));
    System.out.println();

    // hash key field value
     redis.del("home_address");
    redis.hset("home_address", "province", "henan");
    redis.hset("home_address", "city", "sanmenxia");
    redis.hset("home_address", "detail", "what a u doing?");

    redis.hmget("home_address", "province", "city", "detail").toArray().foreach((str) => System.out.print(str + "\t"));
    System.out.println();

  }
}