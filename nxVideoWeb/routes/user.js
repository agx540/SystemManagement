
var express = require('express');
var router = express.Router();

var mongoose = require('mongoose');   
var User = mongoose.model('User');

module.exports = function(){
    
    console.log('Create user rest interface!!');
    

    router.get('/user', function (req, res){
        res.send('Got a GET request at /user');
    });
    
    router.put('/user', function (req, res){
        res.send('Got a PUT request at /user');
    });
   
    router.post('/user', function (req, res){
        if(req.is('application/json')){
            console.log('application/json  /user: ' + req.body.username + ' email: ' + req.body.email);
            
            User.findOne({ 'username': req.body.username}, function(err, user) {
                
                // In case of any error
                if(err){
                    console.log('Error in POST user! ' +  err);
                }
                
                // already exists
                if(user){
                    console.log('User already exists with username: '+ req.body.username);
                }
                else{
                    var newUser = new User();
                    
                    newUser.username = req.body.username;
                    newUser.email = req.body.email;
                    newUser.password = req.body.password;
                    newUser.isAdmin = req.body.isAdmin;
                    
            		// save the user
        			newUser.save(function(err) {
        				if (err){
        					console.log('Error in Saving user: '+err);
                            //TODO: How to handle exceptions?????  
        					throw err;  
        				}
        				console.log(newUser.username + ' stored in db');    
        			});
                }
                
            });

        }
        else{
            console.log('/user: ' + req.body);    
        }
        
        res.send('Got a POST request at /user');
        
    });
    
    router.delete('/user', function (req, res){
        res.send('Got a DELETE request at /user');
    });
    
    console.log('User Rest interface created!!');
    
    return router;
}