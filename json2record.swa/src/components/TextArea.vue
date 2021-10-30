<template>
  <section v-on:keyup="onChange" class="top-section"> 
  <CodeEditor 
    v-model="msg"
    :autofocus="true"
    :hide_header="true"
    :width="'100%'"
    :height="'100%'"
    :border_radius="'0px'"

    ></CodeEditor>
  </section>
</template>

<script>
import CodeEditor from 'simple-code-editor';

export default {
  name: 'TextArea',
  components: {
    CodeEditor
  },
  data: function() {
      return {
        msg: ""
      }
  },
  methods: {
    onChange: function(){
      this.$emit('input-updated', this.msg);
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.top-section {
  width: 33.3%;
  line-height: 1.2;
  position: absolute;
  height: 90%;
  left: 0;
  top: 5%;
  right: 0;
  bottom: 0;
  overflow: auto;
}
</style>
