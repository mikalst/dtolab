<template>
  <b-card class="text">
    {{file}}
  </b-card>
</template>

<script>
export default {
  name: 'DisplayEntity',
  components: {
  },
  props: {
    file: null 
  },
  methods: {
    languages: function () {
      return [
        ['chsarp', 'c#']
      ];
    },
    style: function () {
      console.log(length(this.file));
      return {
        height: length(this.file)
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
  text-align: left;
}
ul {
  text-align: left;
}
.text {
  text-align: left;
  overflow-y: hidden;
  white-space: pre;
}
</style>
