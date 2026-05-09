<template>
  <div class="keys-list">
    <div class="title">Quick animations</div>
    <BorderSVG class="border-upper" />

    <div class="content">
      <Card
        :class="[
          { saved: item.animation !== null },
          { selected: item.key === currentKey },
          'keys-list-slot',
        ]"
        v-for="item in quickAccess"
        :key="item.id"
        :imgPath="item.animation ?
          'https://local.mikzzz.ru/animations/items/' +
            item.animation +
            '.gif' : null
        "
        :title="
          item.animation != null
            ? loc(item.animationName)
            : loc('AnimationsMenu_2')
        "
        @click="onSelect(item.key)"
        :buttonText="item.key"
        :active="item.key === currentKey"
      />
    </div>
    <BorderSVG class="border-lower" />
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import Card from './Card.vue'
import BorderSVG from './BorderSVG.vue'
export default {
  name: 'KeysList',
  computed: {
    ...mapState('animationsMenu', ['quickAccess', 'currentKey']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    ...mapMutations('animationsMenu', ['setCurrentKey']),
    onSelect(key) {
      if (this.currentKey === key) return this.setCurrentKey(null)
      else this.setCurrentKey(key)
    },
  },
  components: { Card, BorderSVG },
}
</script>

<style lang="scss" scoped>
.keys-list {
  position: relative;
  .border-upper {
    top: 0;
    position: absolute;
    width: 16.111vh;
    height: 29.537vh;
  }
  .border-lower {
    bottom: 0;
    position: absolute;
    width: 16.111vh;
    height: 29.537vh;
    transform: scale(1, -1);
  }

  border-image: url(../assets/img/highlight.png);
  width: 29.907vh;
  height: 77.315vh;
  .title {
    position: absolute;
    top: 50%;
    left: 0%;
    transform: translate(-50%, -50%) rotate(-90deg);
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 600;
    font-size: 1.111vh;
    line-height: 1.852vh;
    color: #ffffff;
  }
  align-items: flex-start;
  padding: 2.685vh 0;
  .content {
    display: flex;
    flex-wrap: wrap;
    gap: 3.704vh;
    justify-content: flex-end;
    .card {
      font-size: 1.296vh;
      line-height: 2.222vh;
    }
  }
  .keys-list-slot {
    width: 11.574vh;
    height: 11.574vh;
  }
}
</style>
