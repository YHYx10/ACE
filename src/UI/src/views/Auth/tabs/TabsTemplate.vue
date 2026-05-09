<template>
  <div class="form">
    <div class="form-header">
      <span class="form-title">{{ title }}</span>
      <span class="form-descr">{{ subtitle }}</span>
    </div>
    <div class="form-content">
      <slot></slot>
      <div v-if="isRemember" class="form-save">
        <input type="checkbox" :value="pass" :checked="pass" @input="change" />
        <span>Do you remember the password?</span>
      </div>
    </div>
    <div v-if="notify.status > 0" :class="notify.status == 2 ? 'form-success' : 'form-error'">
      <span>{{ notify.head }}</span>
      <span>{{ notify.msg }}</span>
    </div>
    <DefaultBtn class="form-btn" @click="() => $emit('click-action')">
      {{ btn }}
    </DefaultBtn>
  </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";
import DefaultBtn from '../../UI/button/DefaultBtn.vue';

export default {
    props: {
        title: String,
        subtitle: String,
        btn: String,
        pass: Boolean,
        isRemember: Boolean
    },
    computed: {
        ...mapState("auth", ["notify"]),
        ...mapMutations("auth", ["notifyAdd"]),
    },
    methods: {
        change(e) {
            this.$emit("click-change", e.target.checked);
        },
    },
    components: { DefaultBtn }
};
</script>