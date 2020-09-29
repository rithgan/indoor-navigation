const express = require("express");
let app = express();
const { PythonShell } = require("python-shell");
const multer = require("multer");
const path = require("path");
const { RSA_NO_PADDING } = require("constants");

app.use(express.static(path.join(__dirname, "public")));
app.set("view engine", "pug");

let storage = multer.diskStorage({
  destination: function (req, file, cb) {
    //cb is callback
    cb(null, "public/uploads/");
  },
  filename: function (req, file, cb) {
    let updateName = file["originalname"].split(".");
    updateName[0] = "blueprint";
    updateName[1] = "jpeg"; //renaming all to one coz prog. reading these files is same
    // console.log(updateName.join("."));
    cb(null, updateName.join("."));
  },
});
let upload = multer({
  //multer settings
  storage: storage,
  fileFilter: function (req, file, callback) {
    var ext = path.extname(file.originalname);
    if (ext !== ".png" && ext !== ".jpg" && ext !== ".jpeg") {
      return callback(new Error("Only images are allowed"));
    }
    callback(null, true);
  },
  limits: {
    fileSize: 1024 * 1024,
  },
});

//get route to upload file
app.get("/", (req, res) => {
  res.render("index");
});

//post route to upload file
app.post("/", upload.single("imageupload"), (req, res) => {
  res.redirect("/download");
});

//get route to download json, links to /download
app.get("/download", (req, res) => {
  res.render("download");
});

//get route to download json file, links to /run-script
app.get("/run-script", pythonScript);

function pythonScript(req, res) {
  PythonShell.run("line-detection-algo.py", JSON, function (err, results) {
    if (err) throw err;
    let file = `${__dirname}/line_cord.json`;
    res.download(file);
  });
}
const PORT = process.env.PORT || 3000;

app.listen(PORT, (req, res) => {
  console.log(`Server is listening at port ${PORT}`);
});
