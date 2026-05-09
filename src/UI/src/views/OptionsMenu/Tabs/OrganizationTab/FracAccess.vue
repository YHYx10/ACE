<template>
  <div class="frac-access">
    <div class="frac-access-item" v-for="(acc, key, index) in fraction.access" :key="index">
      <div class="frac-access-name">{{ key }}</div>
      <div class="frac-access-main">
        <div class="frac-access-val">{{ acc }}</div>
        <button class="frac-access-btn" @click="change(key)">
          <img src="/img/optionsMenu/organizationTab/change.svg">
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState, mapMutations} from 'vuex'

export default {
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('optionsMenu', ['fraction'])
  },
  methods: {
    ...mapMutations('optionsMenu', ['setDialog']),
    change(key) {
      this.setDialog({
        input: 'number',
        callback: (val) => {
          val = parseInt(val);
          if (isNaN(val) || val < 0) {
            window.setData('notifyList/notify', {
              type: 1,
              position: 2,
              message: "mmain:frac:dialog:data:wrong",
              time: 3000
            });
          } else {
            window.mp.triggerServer('mmenu:frac:access:change', key, +val);
          }
        },
        value: '',
        placeholder: 'mmain:frac:access:pl',
        tittle: `mmain:frac:access:tit`,
        subtittle: 'mmain:frac:access:sub',
        bg: 'invite'
      });
    }
  },
  mounted() {
    if (this.fraction.canAccess) window.mp.triggerServer("mmenu:frac:access:request")
    //this.$store.commit('optionsMenu/setFractionAccess', {"Access_1": 2,"Access_2": 2,"Access_3": 2,"Access_4": 2});
  }
}
</script>

<style lang="scss" scoped>
.frac-access {
  height: 25rem;
  overflow-y: scroll;
  -ms-overflow-style: none;
  scrollbar-width: none;

  &::-webkit-scrollbar {
    display: none;
  }

  &-item {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 1rem;
    margin-top: 0.25rem;
    color: #fff;
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 40%, rgba(0, 0, 0, 0) 90%) 1;
    background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.03) 35%, rgba(12, 16, 10, 0) 100%);
  }

  &-main {
    display: flex;
    align-items: center;
  }

  &-name {
    font-weight: 600;
  }

  &-val {
    color: #5CFF80;
    margin: 0 3.5rem;
  }

  &-btn {
    width: 1rem;
    height: 1rem;
    background: none;
    opacity: 0.5;

    & img {
      width: 100%;
      height: 100%;
      object-fit: contain;
    }

    &:hover {
      opacity: 1;
    }
  }
}
</style>