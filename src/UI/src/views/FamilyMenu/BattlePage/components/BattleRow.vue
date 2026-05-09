<template>
  <div class="battle-row" :class="data.won ? 'win' : 'lose'">
    <div class="icon">
      <img
        v-if="data.incoming"
        class="bg"
        src="/img/familyMenu/battlePage/icon-attack.png"
        alt=""
      />
      <img
        v-else
        class="bg"
        src="/img/familyMenu/battlePage/icon-defense.png"
        alt=""
      />
      <img
        class="img-ico"
        v-if="data.incoming"
        src="/img/familyMenu/battlePage/icon-attack.png"
      />
      <img class="img-ico" v-else src="/img/familyMenu/battlePage/icon-defense.png" />
    </div>
    <div class="business-name">
      {{ data.bizName }}
    </div>
    <div class="enemy">
      {{ data.enemyName }}
    </div>
    <div class="date">
      {{ data.date }}
    </div>
    <div class="time">
      {{ data.time }}
    </div>
    <div class="weapon">
      {{ loc(getWeaponName(data.weaponName)) }}
    </div>
    <div class="place">
      {{ loc(data.placeName) }}
    </div>
    <div
      class="state"
      :class="[
        { confirmation: data.status === 1 || data.status === 0 },
        data.won ? 'green' : 'red',
      ]"
    >
      <img
        v-if="(data.status === 4 || data.status === 5) && data.won"
        src="/img/familyMenu/battlePage/icon-win.png"
      />
      <img
        v-if="(data.status === 4 || data.status === 5) && !data.won"
        src="/img/familyMenu/battlePage/icon-lose.png"
      />
      <div>{{ loc(getStatusBattle(data.status, data.won)) }}</div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'BattleRow',
  props: {
    data: {
      type: Object,
      required: true,
    },
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', ['weaponsList']),
  },
  methods: {
    getStatusBattle(status, won) {
      switch (status) {
        case 0:
          return 'fam:btl:status:1'
        case 1:
          return 'fam:btl:status:2'
        case 2:
          return 'fam:btl:status:3'
        case 3:
          return 'fam:btl:status:4'
        case 4:
          if (won) return 'fam:btl:status:5'
          else return 'fam:btl:status:6'
        case 5:
          if (won) return 'fam:btl:status:7'
          else return 'fam:btl:status:8'
      }
      return 'Unknown'
    },
    getWeaponName(id) {
      let index = this.weaponsList.findIndex((item) => item.id == id)
      if (index > -1) return this.weaponsList[index].name
      else return 'Unknown'
    },
  },
}
</script>

<style lang="scss" scoped>
.battle-row {
  width: 141.111vh;
  height: 11.574vh;
  overflow: hidden;
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 1.852vh;
  line-height: 2.315vh;
  display: flex;
  align-items: center;
  text-transform: uppercase;
  color: #ffffff;
  position: relative;
  &.lose::before {
    content: '';
    position: absolute;
    width: 33.796vh;
    height: 100%;
    background: linear-gradient(
      90deg,
      rgba(75, 0, 130, 0.15) 0%,
      rgba(75, 0, 130, 0) 100%
    );
  }
  &.win::before {
    content: '';
    position: absolute;
    width: 33.796vh;
    height: 100%;
    background: linear-gradient(
      90deg,
      rgba(11, 69, 23, 0.3) 0%,
      rgba(11, 69, 23, 0) 100%
    );
  }
  &::after {
    content: '';
    position: absolute;
    bottom: 0;
    transform: translate(0, 100%);
    margin-top: 8.704vh;
    width: 141.019vh;
    height: 4.815vh;
    background: rgba(255, 255, 255, 0.55);
    filter: blur(8.241vh);
  }
  .icon {
    width: 19.259vh;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    .bg {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 20.37vh;
      height: 20.37vh;
      opacity: 0.05;
    }
    .img-ico {
      width: 7.315vh;
    }
  }
  .business-name {
    width: 26.019vh;
  }
  .enemy {
    width: 17.5vh;
  }
  .date {
    width: 11.111vh;
  }
  .time {
    width: 9.63vh;
  }
  .weapon {
    width: 13.056vh;
  }
  .place {
    width: 20.556vh;
  }
  .state {
    display: flex;
    flex-direction: column;
    width: 23.426vh;
    font-size: 1.481vh;
    justify-content: center;
    align-items: center;
    img {
      margin-bottom: 0.463vh;
    }
    &.red {
      color: #ff7d7d;
    }
    &.green {
      color: #a0ff98;
    }
    &.confirmation {
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      font-size: 1.481vh;
      line-height: 1.852vh;
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.5);
    }
  }
}
</style>
