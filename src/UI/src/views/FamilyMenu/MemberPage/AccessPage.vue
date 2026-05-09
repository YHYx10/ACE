<template>
  <div class="access-page">
    <div class="members-list">
      <div
        v-for="item of members"
        :key="item.id"
        class="member-item"
        @click="setCurrentMember(item)"
        :class="{ active: item.id === currentMember.id }"
      >
        <div class="online-point"></div>
        <div class="about">
          <div class="name">
            {{ item.nickname }}
          </div>
          <div class="rank">
            {{ getRank(item.rank) && getRank(item.rank).rankName }}
          </div>
        </div>
      </div>
    </div>
    <div class="info">
      <TitleItem
        :name="'The name of the participant'"
        :value="[currentMember.nickname]"
        :fontSize="48"
      />
      <TitleItem
        name="The name of the participant"
        :value="[currentRank.rankName]"
        :fontSize="48"
        class="title-rank"
      />
      <div class="btns">
        <DefaultBtn @click="setModalEditRank">Ändern</DefaultBtn>
        <DefaultBtn @click="setModalLeave" secondary>Spieler entfernen</DefaultBtn>
      </div>
      <div class="title-line">
        <div style="margin-left: 5.37vh" class="text">Zugang Ebene</div>
      </div>
      <div class="description">
 Um Zugangsniveaus zu ändern, gehen Sie zum „Management
Organisation “und dann in die Einstellung„ Titelmanagement “eingeben
      </div>
      <div class="access-list">
        <div class="access-item">
          <div class="name">Zugang zum Haus </div>
          <div class="value">
            {{ loc(accessHouseList.list[currentRank.accessHouse]) }}
          </div>
        </div>
        <div class="access-item">
          <div class="name">Zugang zu den Geschäftskriegen</div>
          <div class="value" :class="{ red: !currentRank.accessWar }">
            {{ switchOn(currentRank.accessWar) }}
          </div>
        </div>
        <div class="access-item">
          <div class="name">Bewegt Möbel</div>
          <div
            class="value"
            :class="{ red: currentRank.accessFurniture === 0 }"
          >
            {{ loc(accessFurnitureList.list[currentRank.accessFurniture]) }}
          </div>
        </div>
        <div class="access-item">
          <div class="name">Zugang zur Kleidung der Organisation</div>
          <div class="value" :class="{ red: !currentRank.accessClothes }">
            {{ switchOn(currentRank.accessClothes) }}
          </div>
        </div>
        <div class="access-item">
          <div class="name">Management der Teilnehmer </div>
          <div class="value" :class="{ red: !currentRank.accessMembers }">
            {{ switchOn(currentRank.accessMembers) }}
          </div>
        </div>
      </div>
      <div class="title-line title-veh">
        <div class="text">Typ Zugang</div>
        <div class="text-second">BRand und Anzahl des Transports</div>
      </div>
      <div class="vehicle-access">
        <div
          v-for="item in currentRank.accessCars"
          :key="item.key"
          class="vehicle-item"
        >
          <div class="access-value">
            {{ loc(accessCarsList.list[item.access]) }}
          </div>
          <div class="vehicle-title">{{ item.carName }}</div>
        </div>
      </div>
    </div>
    <modal-leave
      v-if="modalLeave"
      @setModalLeave="setModalLeave"
      :currentMember="currentMember"
    />
    <modal-edit-rank
      v-if="modalEditRank"
      @setModalEditRank="setModalEditRank"
      :currentMember="currentMember"
      :ranksList="ranksList"
    />
  </div>
</template>

<script>
import DefaultBtn from '../../UI/button/DefaultBtn.vue'
import TitleItem from '../components/TitleItem.vue'
import ModalLeave from './components/ModalLeave'
import ModalEditRank from './components/ModalEditRank'
import { mapState, mapGetters } from 'vuex'

export default {
  name: 'AccessTab',
  components: { TitleItem, DefaultBtn, ModalLeave, ModalEditRank },
  computed: {
    ...mapState('familyMenu', ['isLeader', 'currentRankId']),
    ...mapState('familyMenu/membersPage', ['members']),
    ...mapState('familyMenu/controlPage', [
      'ranksList',
      'accessHouseList',
      'accessFurnitureList',
      'accessCarsList',
    ]),
    ...mapGetters('localization', ['loc']),
    currentRank: function() {
      return this.ranksList.find(
        (element) => element.rankId === this.currentMember.rank
      )
    },
  },
  data: function() {
    return {
      currentMember: {},
      modalLeave: false,
      modalEditRank: false,
    }
  },
  methods: {
    getRank(rankId) {
      return this.ranksList.find((rank) => rank.rankId === rankId)
    },
    switchOn: function(boolean) {
      if (boolean) {
        return 'on'
      } else {
        return 'off'
      }
    },
    setCurrentMember(obj) {
      this.currentMember = obj
    },
    setModalLeave() {
      this.modalLeave = !this.modalLeave
    },
    setModalEditRank: function() {
      this.modalEditRank = !this.modalEditRank
    },
  },

  beforeMount() {
    this.currentMember = { ...this.members[0] }
  },
}
</script>

<style lang="scss" scoped>
div,
input,
button {
  font-family: 'Akrobat';
  font-style: normal;
}
.title-rank {
  margin-top: 1.667vh;
}
.title-line {
  width: 100%;
  // height: 2.963vh;
  position: relative;
  font-weight: 700;
  // transform: translate(0, -50%);
  // box-shadow: 0 0 0 0.093vh red;
  .text {
    font-weight: 700;
    font-size: 2.963vh;
    width: 60%;
    text-transform: uppercase;
  }
  &::before {
    content: '';
    position: absolute;
    width: calc(100% - 0.185vh);
    height: 0.185vh;
    top: 50%;
    transform: translate(0vh, -0vh);
    background: linear-gradient(
      90deg,
      rgba(255, 255, 255, 0.2) 0%,
      rgba(255, 255, 255, 0) 6%,
      rgba(255, 255, 255, 0) 50%,
      rgba(255, 255, 255, 0.2) 100%
    );
  }
}
.access-page {
  margin-top: 2.407vh;
  display: flex;
  width: fit-content;
  gap: 4.537vh;

  .members-list {
    width: 26.204vh;
    height: 65.648vh;
    overflow-y: scroll;
    &::-webkit-scrollbar {
      width: 0.463vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.04);
    }
    &::-webkit-scrollbar-thumb {
      background: #301934 ;
    }
    text-transform: none;
    .member-item {
      border: 0.093vh solid rgba(255, 255, 255, 0.04);
      border-right: 0vh;
      border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.08) 0%,
          rgba(255, 255, 255, 0) 100%
        )
        1 1;
      width: 24.815vh;
      height: 6.481vh;
      margin-bottom: 0.093vh;
      display: flex;
      align-items: center;
      box-sizing: content-box;
      &.active {
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.08) 0%,
          rgba(255, 255, 255, 0) 100%
        );
      }
      &:hover {
        border-image: linear-gradient(
            90deg,
            rgba(255, 255, 255, 0.15) 0%,
            rgba(255, 255, 255, 0) 100%
          )
          1 1;
      }
      .online-point {
        width: 0.741vh;
        height: 0.741vh;
        border-radius: 100%;
        background: #5cff80;
        margin-left: 1.111vh;
        margin-right: 0.833vh;
        margin-bottom: 1.435vh;
      }
      .about {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;

        text-transform: uppercase;
        .name {
          font-size: 2.222vh;
          line-height: 2.685vh;
          color: #ffffff;
        }
        .rank {
          font-size: 1.481vh;
          line-height: 1.759vh;
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.5);
        }
      }
    }
  }
  .info {
    width: 110.926vh;
    height: 64.444vh;
    .btns {
      margin-top: 1.852vh;
      display: flex;
      gap: 0.926vh;
      margin-bottom: 4.074vh;
      button {
        width: 27.315vh;
        height: 6.944vh;
        color: #ffffff;
        font-weight: 700;
        font-size: 2.222vh;
      }
    }
    .description {
      margin-top: 1.389vh;
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.5);
    }

    .access-list {
      margin-top: 3.981vh;
      margin-left: 3.519vh;
      margin-bottom: 4.352vh;
      display: flex;
      gap: 4.537vh;
      font-family: 'Akrobat';
      text-transform: uppercase;
      .access-item {
        display: flex;
        flex-direction: column;
        gap: 0.093vh;
        font-weight: 700;
        font-size: 1.481vh;
        line-height: 1.759vh;
        color: rgba(255, 255, 255, 0.5);
        .name {
          font-size: 1.481vh;
          line-height: 1.759vh;
          color: rgba(255, 255, 255, 0.5);
        }
        .value {
          font-size: 2.222vh;
          line-height: 2.685vh;
          color: #a0ff98;
          &.red {
            color: #ff7d7d;
          }
        }
      }
    }

    .title-veh {
      width: 25.093vh;
      position: relative;
      font-size: 2.222vh;
      .text {
        margin-left: 3.519vh;
        font-size: 2.222vh;
      }
      .text-second {
        margin-left: 1.574vh;
        width: 25.463vh;
        top: 50%;
        left: 100%;
        transform: translate(0, -50%);
        position: absolute;
      }
    }

    .vehicle-access {
      display: flex;
      flex-direction: column;
      gap: 0.648vh;
      height: 12.778vh;
      text-transform: uppercase;
      margin-top: 2.315vh;
      overflow-y: auto;
      &::-webkit-scrollbar {
        width: 0.463vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.04);
      }
      &::-webkit-scrollbar-thumb {
        background: #301934 ;
      }
      .vehicle-item {
        display: flex;
        font-weight: 700;
        font-size: 1.667vh;
        line-height: 2.037vh;
        .access-value {
          margin-left: 3.519vh;
          width: 21.574vh;
          color: #a0ff98;
        }
        .vehicle-title {
          color: #ffffff;
          margin-left: 1.574vh;
        }
      }
    }
  }
}
</style>
