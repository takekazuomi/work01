# https://bugs.mysql.com/bug.php?id=109670
build:
	dotnet publish --os linux --arch x64 /p:PublishSingleFile=true -c Release -o out

run:
	docker run -it --rm -p 8080:80 dotnet-web:1.0.0
	
