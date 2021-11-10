<template>
  <html>
    <Banner @download="download" class="header"/>
    <body> 
      <InputArea 
        @input-updated="startTimer"
        class="left"
        />
      <DisplayArea 
        v-bind:files="response.files" 
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
      input: null,
      timePassed: 0,
      timerInteral: null
    };
  },
  methods: {
    startTimer(input) {
      this.input = input;
      if (this.timerInteral) {
        this.timePassed = 0;
      }
      else {
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
      var url = process.env.VUE_APP_PATH+"csharp";
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
        }                
        })
      .then(response => {
        console.log(response);
        this.response = response;
        this.files = response.files;
      })
      .catch(err => {
        console.error(err);
      });
    },
    download: function() {
      console.log(Object.keys(this.files).length);
      Object.keys(this.files).forEach((key, index) => {
        console.log(key, index);
        var fileURL = window.URL.createObjectURL(new Blob([this.files[key]]));
        var fileLink = document.createElement('a');

        fileLink.href = fileURL;
        fileLink.setAttribute('download', key+'.cs');
        document.body.appendChild(fileLink);

        fileLink.click();
      });
    }
  }
}
</script>

<style>
#app {
  font-family: 'Roboto Condensed',sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  -webkit-text-size-adjust: 100%;
  height: 100%;
  width: 100%;
  text-align: center;
  color: #2c3e50;
}
:root {
    --highlight-color: #efefef;
    --header-color: #abb2bf;
    --header-text-color: #282c34;
    --border-color: #aaa;
    --footer-color: #eee;
}
html {
  height: 100%;
  display: flex;
  flex-direction: column;
  padding-top: 0;
  margin-top: 0;
}
.header {
  flex: 0 1 auto;
}
body {
  flex: 1 1 auto;
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100%;
  margin: 0;
}
.left {
  width: 50%;
  flex: 0 1 auto;
}
.right {
  width: 50%;
  flex: 0 1 auto;
}
</style>
