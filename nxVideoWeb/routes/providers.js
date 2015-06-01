var express = require('express');
var router = express.Router();

var mongoose = require('mongoose');   
var Provider = mongoose.model('Provider');

module.exports = function(){
	
	router.get('/provider', function(req, res){
		
		Provider.find().exec(function(err, doc){
			console.log('return json on GET provider: ' + doc);
			res.json(doc);
		});
	});
	
	console.log('Prpvider Rest interface created!!');
	
	return router;
}
