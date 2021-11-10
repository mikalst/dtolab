<template>
  <section v-on:keyup="onChange">
  <CodeEditor 
    v-model="msg"
    :autofocus="true"
    :width="'100%'"
    :height="'100%'"
    :border_radius="'0px'"

    ></CodeEditor>
  </section>
</template>

<script>
import CodeEditor from 'simple-code-editor';

export default {
  name: 'InputArea',
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
section {
  line-height: 1.0;
}
</style>
