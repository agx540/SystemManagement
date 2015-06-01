;/* global __dirname */
var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');


var dbschema_database = require('./database/dbschema');
//add user routing
var user_routes = require('./routes/user')();
//add provider routing
var provider_routes = require('./routes/providers')();

var app = express();


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

// uncomment after placing your favicon in /public
//app.use(favicon(__dirname + '/public/favicon.ico'));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());

app.use(express.static(path.join(__dirname, 'public')));

app.use('/', user_routes);
app.use('/', provider_routes);




var http = require('http');

//The url we want is: 'www.random.org/integers/?num=1&min=1&max=10&col=1&base=10&format=plain&rnd=new'
var options = {
  host: 'localhost',
  path: '/Provider/Status',
  port: '9001'
  
};

var mongoose = require('mongoose');   
var Provider = mongoose.model('Provider');

callback = function(response) {
  var str = '';

  //another chunk of data has been recieved, so append it to `str`
  response.on('data', function (chunk) {
    str += chunk;
  });

  //the whole response has been recieved, so we just print it out here
  response.on('end', function () {
    console.log(str);
    var provider = new Provider();
    var parsed = JSON.parse(str);
    
    provider.name = parsed.ProviderSystemName;
    provider.operationMode = parsed.operationMode;
    provider.operationState = parsed.operationState;

    for(var i = 0 ; i < parsed.CameraStates.length; i++){
        provider.cameras.push({
            'name': parsed.CameraStates[i].CameraSystemName,
            'operationMode': parsed.CameraStates[i].CameraRecordingMode,
            'operationState': parsed.CameraStates[i].CameraOperationState   
            });
    };
    
	// save provider state
	provider.save(function(err) {
		if (err){
			console.log('Error in Saving provider state: '+err);
            //TODO: How to handle exceptions?????  
			throw err;  
		}
		console.log(provider.name + ' stored in db');   
	});
    
  });
}

//http.request(options, callback).end();
//console.log('------------------');
//options.port = '9002';
//http.request(options, callback).end();
//console.log('------------------');
//options.port = '9003';
//http.request(options, callback).end();
//console.log('------------------');
//options.port = '9004';
//http.request(options, callback).end();
//console.log('------------------');
//options.port = '9005';
//http.request(options, callback).end();
//console.log('------------------');

app.get('/', function (req, res) {
  res.send('Hello World!');
});




console.log('create error handler');

// catch 404 and forward to error handler
app.use(function (req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function (err, req, res, next) {
        res.status(err.status || 500);
        //TODO: error file not found
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function (err, req, res, next) {
    res.status(err.status || 500);
    //TODO: error file not found
    res.render('error', {
        message: err.message,
        error: {}
    });
});

console.log('error handler created');

console.log('app script ready!!!!');



module.exports = app;
