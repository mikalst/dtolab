<template>
  <section v-on:keyup="onChange">
    <b-form-textarea 
      v-model="msg"
      :autofocus="true"
      rows="3"
      max-rows="8"
      no-auto-shrink
      @keydown.tab.prevent="tabber($event)"
      class="bg-dark text-white textarea"
    ></b-form-textarea >
  </section>
</template>

<script>

export default {
  name: 'InputArea',
  components: {
  },
  data: function() {
      return {
        msg: ""
      }
  },
  methods: {
    onChange: function(){
      this.$emit('input-updated', this.msg);
    },
    tabber (event) {
      if (event) {
        let text = this.msg,
        originalSelectionStart = event.target.selectionStart,
        textStart = text.slice(0, originalSelectionStart),
        textEnd =  text.slice(originalSelectionStart);

        this.msg = `${textStart}\t${textEnd}`
        event.target.value = this.msg // required to make the cursor stay in place.
        event.target.selectionEnd = event.target.selectionStart = originalSelectionStart + 1
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.textarea {
  height: 100%;
}
</style>
