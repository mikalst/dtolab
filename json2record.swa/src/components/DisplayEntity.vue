<template>
  <div class="header">
    {{file.name}}
  </div>
  <ul> 
  <li v-for="row in file.attributes" :key="row.name">
    {{row.name}},{{row.datatype}},{{row.isList}}
  </li>
  </ul>
</template>

<script>
export default {
  name: 'DisplayEntity',
  props: {
    file: {} 
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
  margin-left: 3%;
  text-align: left;
}
ul {
  text-align: left;
}

</style>
