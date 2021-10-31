<template>
  <header class="banner">
    <h1>Json2Struct</h1>
    <button v-on:click="download"> <h1> Download </h1> </button>
  </header>
</template>

<script>
export default {
  name: 'Banner',
  methods: {
    download: function() {
      this.$emit("download");
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.banner{
  width: 100%;
  text-align: left;
  padding-top: .25%;
  padding-bottom: 0.25%;
  color: var(--header-text-color);
  background-color: var(--header-color);
  position: relative;
  display: flex;
  align-items: center;
}
h1 {
  margin-left: 0.5%;
  font-size: 100%;
  width: 91%;
}
button {
  align-content: right;
  margin-right: 0.5%;
  width: 8%;
}
</style>
