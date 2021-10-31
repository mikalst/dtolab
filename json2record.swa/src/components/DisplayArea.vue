<template>
  <div class="outer">
  <DisplayEntity 
    v-for="value in files" :key="value" 
    :file="value"/>
  </div>
</template>

<script>
import DisplayEntity from './DisplayEntity.vue'

export default {
  name: 'DisplayArea',
  components: {
    DisplayEntity
  },
  props: {
    files: {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.outer {
  width: 48%;
  line-height: 1.2;
  position: absolute;
  height: 90%;
  left: 50%;
  top: 5%;
  right: 0;
  bottom: 0;
  overflow: auto;
}
</style>
