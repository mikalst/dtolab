<template>
  <Banner @download="download" />
  <body> 
    <main>
      <InputArea 
        @input-updated="callapi"
        />
      <DisplayArea v-bind:files="files" />
    </main>
  </body>
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
      "response": {},
      "files": {} 
    };
  },
  methods: {
    callapi: function(input) {
      fetch("http://localhost:7071/api/parse/csharp", {
        "method": "POST",
        "body": input
      })
      .then(response => { 
        if(response.ok){
            return response.json()    
        } else{
            alert("Server returned " + response.status + " : " + response.statusText);
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
  line-height: 1.15;
  text-align: center;
  color: #2c3e50;
}
:root {
    --highlight-color: #efefef;
    --header-color: burlywood;
    --header-text-color: #000;
    --border-color: #aaa;
    --footer-color: #eee;
}
body {
  margin: 0;
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
}
main {
  display: flex;
  flex: 1 0 0;
}
</style>
