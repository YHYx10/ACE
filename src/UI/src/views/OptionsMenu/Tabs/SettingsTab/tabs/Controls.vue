<template>
  <div class="settings-controls">
    <div
        class="billet-item"
        v-for="(button, index) in buttons"
        :key="index"
        @click="changeButton(button)"
    >
      <div class="title">{{ loc(button.text) }}</div>
      <div class="btn">{{ getTag(button.key) }}</div>
    </div>
  </div>
</template>

<script>
import {mapState, mapMutations, mapGetters} from "vuex";

export default {
  name: "Controls",
  computed: {
    ...mapState("optionsMenu", ["buttons", "keyTags"]),
    ...mapGetters('localization', ['loc'])
  },
  methods: {
    ...mapMutations("optionsMenu", ["setDialog"]),
    getTag(key) {
      for (const tKey in this.keyTags) {
        if (this.keyTags[tKey] == key) return tKey;
      }
      return "unk"
    },
    changeButton(button) {
      //if (button.lock) return;
      this.setDialog({
        callback: (val) => {
          if (!val || val == '') {
            window.mp.trigger("cef:mmenu:action:key:wrang", button.name, val);
            return;
          }
          const exists = this.buttons.find((b) => b.key && b.key == val);
          if (exists)
            window.setData("notifyList/notify", {
              type: 1,
              position: 2,
              message: "mmain:control:key:exists",
              time: 3000,
            });
          else window.mp.trigger("cef:mmenu:action:key:bind", button.name, val);
        },
        value: "",
        tittle: `mmain:controls:key:tit`,
        subtittle: "mmain:controls:key:sub",
        bg: "invite",
        keyHandler: true,
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.settings-controls {
  .billet-item {
    .btn {
      font-weight: 700;
      text-align: center;
      min-width: 3rem;
      padding: 0.2rem 0.8rem;
      background: #301934 ;
      cursor: pointer;
      &.selected {
        background: rgba(92, 255, 128, 0.25);
        border: 1px solid #5CFF80;
      }
    }
  }
}

</style>
