var mongoose = require('mongoose');

exports.mongoose = mongoose;

// Database connect
var uristring = 
  process.env.MONGOLAB_URI || 
  process.env.MONGOHQ_URL || 
  'mongodb://localhost/test';

var mongoOptions = { db: { safe: true }};

mongoose.connect(uristring, mongoOptions, function (err, res) {
  if (err) { 
    console.log ('ERROR connecting to: ' + uristring + '. ' + err);
  } else {
    console.log ('Successfully connected to: ' + uristring);
  }
});

//******* Database schema TODO add more validation
var Schema = mongoose.Schema, 
	ObjectId = Schema.ObjectId;

// User schema
var userSchema = new Schema({
  username: { type: String, required: true, unique: true },
  email: { type: String, required: true, unique: true },
  password: { type: String, required: true},
  isAdmin: { type: Boolean, required: true },
});


// Bcrypt middleware
//TODO

// Password verification
//TODO

// Export user model
var userModel = mongoose.model('User', userSchema);
exports.userModel = userModel;
