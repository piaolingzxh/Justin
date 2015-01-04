//redis -> jedis
//驱动地址：https://raw.githubusercontent.com/xetorthio/jedis/mvn-repo/redis/clients/jedis/2.2.0/jedis-2.2.0.jar
//或者http://files.cnblogs.com/piaolingzxh/jedis-2.2.0.jar.zip
import java.util.List;

import redis.clients.jedis.*;

public class redis_jedis {

	public static void main(String[] args) {
		Jedis redis = new Jedis("localhost", 6379);// 连接redis
		// String
		redis.del("user");
		redis.set("user", "piaolingzxh");
		System.out.println(redis.get("user"));
		// List
		redis.del("edu");
		redis.lpush("edu", "xiaoxue", "chuzhong", "gaozhong");
		List<String> eduList = redis.lrange("edu", 0, -1);
		for (int i = 0; i < eduList.size(); i++) {
			System.out.print(eduList.get(i) + "\t");
		}
		System.out.println();

		// hash key field value
		redis.del("home_address");
		redis.hset("home_address", "province", "henan");
		redis.hset("home_address", "city", "sanmenxia");
		redis.hset("home_address", "detail", "what a u doing?");

		List list = redis.hmget("home_address", "province", "city", "detail");
		for (int i = 0; i < list.size(); i++) {
			System.out.print(list.get(i) + "\t");
		}	 
	}
}
