<template>
  <Banner />
  <body> 
    <main>
      <TextArea 
        @input-updated="callapi"
        />
      <DisplayArea v-bind:files="files" />
    </main>
  </body>
</template>

<script>
import TextArea from './components/TextArea.vue'
import DisplayArea from './components/DisplayArea.vue'
import Banner from './components/Banner.vue'

export default {
  name: 'App',
  components: {
    TextArea,
    DisplayArea,
    Banner
  },
  data: function() {
    return {
      "files": {} 
    };
  },
  methods: {
    callapi: function(input) {
      fetch("http://localhost:7071/api/parse", {
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
        this.files = response.files;
      })
      .catch(err => {
        console.error(err);
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
