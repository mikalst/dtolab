<template>
  <html>
    <Banner 
      @download="download"
      @name_changed="updatename"
      @namespace_changed="update_namespace"
      @classtype_changed="update_classtype" class="header"/>
    <body> 
      <InputArea 
        @input-updated="startTimer"
        class="left"
        />
      <DisplayArea 
        v-bind:files="response?.files" 
        class="right"/>
    </body>
  </html>
</template>

<script>
import InputArea from './components/InputArea.vue'
import DisplayArea from './components/DisplayArea.vue'
import Banner from './components/Banner.vue'


export default {
  name: 'App',
  components: {
    InputArea,
    DisplayArea,
    Banner
  },
  data: function() {
    return {
      response: {},
      input: "",
      timePassed: 0,
      timerInteral: null,
      filename: "dto",
      namespace: "ns",
      classtype: "class"
    };
  },
  methods: {
    startTimer(input) {
      this.input = input;
      if (this.timerInteral) {
        this.timePassed = 0;
      }
      else if (this.input.length > 0) {
        this.timerInteral = setInterval(() => { 
          if (this.timePassed == 500) {
            this.stopTimer();
            this.timePassed = 0;
            this.callapi(this.input);
          }
          else {
            this.timePassed += 100;
          }
        }, 100);
      }
    },
    stopTimer() {
      clearInterval(this.timerInteral);
      this.timerInteral = null;
    },
    callapi: function(input) {
      var url = `${process.env.VUE_APP_PATH}csharp`
        + `?name=${this.filename}`
        + `&ns=${this.namespace}`
        + `&classtype=${this.classtype}`;
      console.log(`Fetching from ${url}`);
      fetch(url, {
        "method": "POST",
        "body": input
      })
      .then(response => { 
        if(response.ok){
            return response.json()    
        }
        else{
            console.error("Server returned " + response.status + " : " + response.statusText);
            return null;
        }                
        })
      .then(response => {
        this.response = response;
      })
      .catch(err => {
        console.error(err);
      });
    },
    download: function() {
      Object.keys(this.response.files).forEach((key, index) => {
        console.log(key, index);
        var fileURL = window.URL.createObjectURL(new Blob([this.response.files[key]]));
        var fileLink = document.createElement('a');

        fileLink.href = fileURL;
        var fileName = key[0].toUpperCase() + key.substring(1) + '.cs';
        fileLink.setAttribute('download', fileName);
        document.body.appendChild(fileLink);

        fileLink.click();
      });
    },
    updatename: function(filename) {
      this.filename = filename;
      this.startTimer(this.input);
    },
    update_namespace: function(namespace) {
      this.namespace = namespace;
      this.startTimer(this.input);
    },
    update_classtype: function(classtype) {
      this.classtype = classtype;
      this.startTimer(this.input);
    }
  }
}
</script>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  -webkit-text-size-adjust: 100%;
  height: 100%;
  width: 100%;
  text-align: center;
  font-size: 0.65rem;
  color: #2c3e50;
}
h1 {
  font-family: Consolas, Monaco, monospace;
  align-content: center;
  vertical-align: center;
}
input, button, select {
  font-family: Consolas, Monaco, monospace;
  margin-left: 0.5em;
}
html {
  height: 100%;
  display: flex;
  flex-direction: column;
  padding-top: 0;
  margin-top: 0;
}
body {
  flex: 1 1 auto;
  display: flex;
  flex-direction: row;
  overflow-y: hidden;
  height: 95%;
  width: 100%;
  margin: 0;
}
.header {
  display: flex;
  flex: 0 1 auto;
  /* max-height: 5%; */
}
.left {
  width: 50%;
  flex: 0 1 auto;
  overflow: scroll;
}
.right {
  width: 50%;
  flex: 0 1 auto;
  overflow: scroll;
}
</style>
