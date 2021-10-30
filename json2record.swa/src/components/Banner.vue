<template>
  <header class="banner">
    <h1>Json2Struct</h1>
  </header>
</template>

<script>
export default {
  name: 'Banner',
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.banner{
  width: 100%;
  text-align: left;
  padding: 5px; 
  padding-left: 7px;
  color: var(--header-text-color);
  background-color: var(--header-color);
  position: relative;
  display: flex;
  align-items: center;
}
h1 {
  font-size: 100%;
}
</style>
