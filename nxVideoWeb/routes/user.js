
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
            
            User.findOne({ $or: [{'username': req.body.username}, {'email': req.body.email}] }, 
                        function(err, user) {
                
                // In case of any error
                if(err){
                    console.log('Error in POST user! ' +  err);
                }
                
                // already exists
                var returnState = "";
                if(user){
                    if(user.username == req.body.username){
                        returnState = 'Username already in use: '+ req.body.username;
                        console.log('Username already in use: '+ req.body.username);
                    }
                    if(user.email == req.body.email){
                        returnState += 'Email already in use: '+ req.body.email;
                        console.log('Email already in use: '+ req.body.email);
                    }
                    res.send({state: 'failure', user: null, message: returnState});
                    
                }
                else{
                    var newUser = new User();
                    
                    newUser.username = req.body.username;
                    newUser.email = req.body.email;
                    newUser.password = req.body.password;
                    //TODO: add admin flag in complete stack
                    newUser.isAdmin = false;
                    
            		// save the user
        			newUser.save(function(err) {
        				if (err){
        					console.log('Error in Saving user: '+err);
                            //TODO: How to handle exceptions?????  
        					throw err;  
        				}
        				console.log(newUser.username + ' stored in db');   
                        
                        res.send({state: 'success', user: newUser}); 
        			});
                }
                
            });

        }
        else{
            console.log('/user: ' + req.body);    
        }
        
        
        
    });
    
    router.delete('/user', function (req, res){
        res.send('Got a DELETE request at /user');
    });
    
    console.log('User Rest interface created!!');
    
    return router;
}