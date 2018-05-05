
# TaskList
This is a web system aimed to help you to control all of your activities 

## API call samples

**Add task**
```
curl -d '{"Title":"My first task", "Description":"A big task"}' \ 
-H "Content-Type: application/json" -X POST http://localhost:3000/api/tasks
```

**List tasks**
```
curl -H "Accept:application/json" -X GET http://localhost:58865/api/tasks
```

**Update task**
```
curl -d '{"Completed": true}' -H "Content-Type: application/json" \
-X PUT http://localhost:58865/api/tasks/1
```

**Delete task**
```
curl -X DELETE http://localhost:58865/api/tasks/1
```
