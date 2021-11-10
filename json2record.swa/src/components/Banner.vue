<template>
  <header>
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
header{
  text-align: left;
  display: flex;
  color: var(--header-text-color);
  background-color: var(--header-color);
  padding: 0.5vh;
}
h1 {
  flex: 1 1 auto;
  font-size: 100%;
}
button {
  flex: 0 1 auto;
  align-content: right;
  background-color: var(--header-color);
  border: 2px solid var(--header-text-color);
  color: var(--header-text-color);
}
</style>
