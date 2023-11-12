package com.goat.deathnote.domain.log.controller;

import com.goat.deathnote.domain.log.dto.LogPostDto;
import com.goat.deathnote.domain.log.entity.Log;
import com.goat.deathnote.domain.log.service.LogService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/logs")
public class LogController {

    private final LogService logService;

    @PostMapping
    public ResponseEntity<Log> createLog(@RequestBody LogPostDto logPostDto) {
        Log log = logService.saveLog(logPostDto);
        return ResponseEntity.ok(log);
    }

    @GetMapping
    public ResponseEntity<List<Log>> getAllLogs() {
        return ResponseEntity.ok(logService.getAllLogs());
    }

//    @GetMapping("/{id}")
//    public ResponseEntity<Optional<Log>> getLogById(@PathVariable Long id) {
//        return ResponseEntity.ok(logService.getLogById(id));
//    }

    //닉네임으로 로그찾기
    @GetMapping("/{nickname}")
    public ResponseEntity<?> getLogByNickname(@PathVariable String nickname) {
        return ResponseEntity.ok(logService.getLogByNickname(nickname));
    }

}
